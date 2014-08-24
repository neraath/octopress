<?php
namespace My\Controller\Plugin;

use \Doctrine\ORM\EntityManager,
    \My\Entity\Repository\CustomerRepository,
    \My\Auth\Adapter;

/**
 * HTTP Basic Authentication Plugin for My API
 */
class BasicAuthPlugin extends \Zend_Controller_Plugin_Abstract
{
    const AUTHORIZATION_HEADER = 'Authorization';
    const IDENTITY_KEY = 'Identity';

    /** @var \Doctrine\ORM\EntityManager */
    private $_entityManager;

    /** @var \My\Entity\Repository\CustomerRepository */
    private $_customerRepository;

    public function __construct(EntityManager $entityManager, CustomerRepository $customerRepository)
    {
        $this->_entityManager = $entityManager;
        $this->_customerRepository = $customerRepository;
    }

    public function preDispatch(\Zend_Controller_Request_Abstract $request)
    {
        $authorizationHeader = $request->getHeader(self::AUTHORIZATION_HEADER);
        if ($authorizationHeader == null || $authorizationHeader == '') {
            $this->_redirectNoAuth($request);
            return;
        }

        // The header needs to be base64 decoded, then match the regex in order to proceed.
        $authorizationHeader = base64_decode($authorizationHeader);
        if (!preg_match('/[^\:]*\:.*/i', $authorizationHeader)) {
            $this->_redirectInvalidRequest($request);
            return;
        }

        $authorizationParts = explode(':', $authorizationHeader);
        $username = $authorizationParts[0];
        $password = $authorizationParts[1];

        // Authenticate.
        try {
            $authAdapter = new \My\Auth\Adapter($this->_customerRepository, $username, $password);
            if ($authAdapter->authenticate() != \Zend_Auth_Result::SUCCESS) {
                $this->_redirectNoAuth($request);
                return;
            }

            // Get the user and set their identity in the registry.
            $user = $this->_customerRepository->findOneByUsername($username);
            \Zend_Registry::set(self::IDENTITY_KEY, $user);
        } catch (\Exception $e) {
            $this->_redirectNoAuth($request);
            return;
        }
    }

    /**
     * Redirects users to our no authentication error page.
     *
     * @param \Zend_Controller_Request_Abstract $request
     * @return mixed
     */
    protected function _redirectNoAuth(\Zend_Controller_Request_Abstract $request)
    {
        if ($request->getModuleName() == 'api' &&
            $request->getControllerName() == 'error' &&
            $request->getActionName() == 'noauth') {
            return;
        }

        // Forward the request to the noauth error page.
        $request->setModuleName('api')
                ->setControllerName('error')
                ->setActionName('noauth');
    }

    /**
     * Redirects users to our invalid request error page.
     *
     * @param \Zend_Controller_Request_Abstract $request
     * @return mixed
     */
    protected function _redirectInvalidRequest(\Zend_Controller_Request_Abstract $request)
    {
        if ($request->getModuleName() == 'api' &&
            $request->getControllerName() == 'error' &&
            $request->getActionName() == 'invalid') {
            return;
        }

        // Forward the request to the noauth error page.
        $request->setModuleName('api')
                ->setControllerName('error')
                ->setActionName('invalid')
                ->setParam('message', 'Invalid authentication header.');
    }
}

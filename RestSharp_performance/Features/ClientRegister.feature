Feature: Client Registration and Login Load Testing

  Scenario: Perform load testing on client registration with random data
    Given I have random registration data
    When I send 10 concurrent requests to "/client_register"
    Then I should receive a successful response for all requests


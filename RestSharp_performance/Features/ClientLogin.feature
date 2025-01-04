Feature: Client Login Load Testing

  Scenario: Perform load testing on client login with random data
    Given I have random login data
    When I send 10 concurrent requests to "/client_login"
    Then I should receive a successful response for all requests

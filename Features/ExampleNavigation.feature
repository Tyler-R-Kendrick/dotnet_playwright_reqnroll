Feature: Example Web Navigation
  As a test automation engineer
  I want to navigate to a website and interact with it
  So that I can verify the application works correctly

  Scenario: Navigate to example website
    Given I navigate to "https://example.com"
    Then I should see the page title contains "Example Domain"
    
  Scenario: Search functionality
    Given I navigate to "https://example.com"
    When I click on a link with text "More information"
    Then I should be navigated to a new page

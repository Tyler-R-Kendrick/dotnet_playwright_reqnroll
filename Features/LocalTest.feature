Feature: Local HTML Testing
  As a test automation engineer
  I want to test with local HTML content
  So that I can verify the framework works without external dependencies

  Scenario: Test with inline HTML
    Given I navigate to a page with HTML content
    Then I should see "Hello World" on the page

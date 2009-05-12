namespace xUnit.GWT.Example
{
    [As_A("User...")]
    [I_Want("To...")]
    [So_That("I...")]
    //[Story("As a user... I want to... So that I...")]
    public class This_is_my_story : This_is_my_story_Base
    {
        [Scenario]
        public void when_transferring_between_two_accounts()
        {
            Given("both accounts have positive balances");            
            When("transfer money from a to b");
            Then("a should have the total balance of b", () => a.Balance.ShouldBe(3));
            Then("balance of b should be 0", () => b.Balance.ShouldBe(0));
        }

        [Scenario]
        public void if_then_is_wrong_then_it_should_fail()
        {
            Given("both accounts have positive balances");
            When("transfer money from a to b");
            Then("a should have the total balance of b", () => a.Balance.ShouldBe(3));
            Then("balance of b should be 0", () => b.Balance.ShouldBe(3));
        }

        [Scenario]
        public void part_is_missing()
        {
            Given("one valid given block", Pending);
            When("there is no then block [then it should fail]", Pending);
            //Then("it should fail");
        }

        [Scenario]
        public void test_marked_as_pending()
        {
            Given("one pending block", Pending);
            Then("it should be marked as pending in the report");
        }

        [Scenario]
        public void pending_block()
        {
            Given("a block which has been defined");
            When("no action assigned", Pending);
            Then("it should be marked as pending");
        }
    }

    public class This_is_my_story_Base : Story
    {
        protected Account a;
        protected Account b;

        public This_is_my_story_Base()
        {
            SetGiven("both accounts have positive balances", () =>
                                                                 {
                                                                     a = new Account();
                                                                     a.Balance = 1;
                                                                     b = new Account();
                                                                     b.Balance = 2;
                                                                 });

            SetWhen("transfer money from a to b", () =>
                                                      {
                                                          a.Balance += b.Balance;
                                                          b.Balance = 0;
                                                      });
        }
    }

    public class Account
    {
        public int Balance;
    }
}
<template>
  <div class="row">
    <div class="col">
      <div class="container">
        <div class="row row-cols-1 g-3">
          <div
            class="col"
            v-for="catergory in budget.categories"
            :key="catergory"
          >
            <div class="card">
              <div class="card-header">{{ catergory }}</div>
              <ul class="list-group list-group-flush">
                <li
                  class="list-group-item"
                  v-for="(item, idx) in budget.items.filter(
                    (item) => item.catergory === catergory
                  )"
                  :key="idx"
                >
                  <div class="row">
                    <div class="col">
                      <h6>{{ catergory }}</h6>
                    </div>
                    <div class="col">
                      <h6>Planned Amount</h6>
                    </div>
                    <div class="col">
                      <h6 @click="totalRemainingView = !totalRemainingView">
                        {{
                          totalRemainingView
                            ? "Total Remaining"
                            : "Total Received"
                        }}
                      </h6>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col">
                      <input
                        type="text"
                        class="form-control"
                        v-model="item.name"
                      />
                    </div>
                    <div class="col">
                      <input
                        type="text"
                        class="form-control"
                        v-model="item.plannedAmount"
                      />
                    </div>
                    <div class="col">
                      {{
                        totalRemainingView
                          ? totalRemaining(item)
                          : totalSpend(item)
                      }}
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BudgetIndexView",
  data() {
    return {
      totalRemainingView: true,
      budget: {
        month: "January",
        categories: ["Income", "Giving", "Housing", "Food", "Subsriptions"],
        transactions: [
          {
            type: "Income",
            date: "2023-01-01",
            vendor: "Work",
            item: "Income",
            amount: 1001,
            number: "number",
            notes: ""
          }
        ],
        items: [{ name: "Income", catergory: "Income", plannedAmount: 2000 }]
      }
    };
  },
  methods: {
    totalSpend(item) {
      return this.budget.transactions
        .filter((transaction) => {
          return transaction.item === item.name;
        })
        .map((transaction) => {
          return transaction.amount;
        })
        .reduce((accumulator, currentValue) => {
          return accumulator + currentValue;
        }, 0);
    },
    totalRemaining(item) {
      return item.plannedAmount - this.totalSpend(item);
    }
  },
  computed: {
    incomeTotal() {
      return this.budget.transactions
        .filter((transaction) => {
          return transaction.catergory === "Income";
        })
        .map((transaction) => {
          return transaction.amount;
        })
        .reduce((accumulator, currentValue) => {
          return accumulator + currentValue;
        }, 0);
    },
    expenseTotal() {
      return this.budget.transactions
        .filter((transaction) => {
          return transaction.catergory !== "Income";
        })
        .map((transaction) => {
          return transaction.amount;
        })
        .reduce((accumulator, currentValue) => {
          return accumulator + currentValue;
        }, 0);
    },
    isBalanced() {
      return this.incomeTotal === this.expenseTotal;
    }
  }
};
</script>

<template>
  <div class="row">
    <div class="col">
      <div class="container">
        <div class="row">
          <div class="col">
            <button
              class="btn btn-primary"
              data-bs-toggle="modal"
              data-bs-target="#transaction-modal-form"
            >
              Add Transaction
            </button>
            <div
              class="modal fade"
              id="transaction-modal-form"
              tabindex="-1"
              aria-labelledby="exampleModalLabel"
              aria-hidden="true"
            >
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">
                      Modal title
                    </h1>
                  </div>
                  <form @submit.prevent="addTranzaction()">
                    <div class="modal-body">
                      <div class="row">
                        <div class="col-auto">
                          <div class="form-check">
                            <input
                              class="form-check-input"
                              type="radio"
                              id="expense-radio"
                              name="transaction-type-radio"
                              value="Expense"
                              v-model="newTransaction.type"
                            />
                            <label class="form-check-label" for="expense-radio">
                              Expense
                            </label>
                          </div>
                        </div>
                        <div class="col-auto">
                          <div class="form-check">
                            <input
                              class="form-check-input"
                              type="radio"
                              id="income-radio"
                              name="transaction-type-radio"
                              value="Income"
                              v-model="newTransaction.type"
                            />
                            <label class="form-check-label" for="income-radio">
                              Income
                            </label>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col">
                          <input
                            class="form-control"
                            type="number"
                            v-model="newTransaction.amount"
                          />
                        </div>
                      </div>
                      <div class="row">
                        <div class="col">
                          <input
                            type="date"
                            class="form-control"
                            v-model="newTransaction.date"
                          />
                        </div>
                        <div class="col">
                          <input
                            type="text"
                            class="form-control"
                            v-model="newTransaction.vendor"
                          />
                        </div>
                      </div>
                      <div class="row">
                        <select
                          class="form-select"
                          v-model="newTransaction.item"
                        >
                          <option v-for="item in itemNames" :key="item">
                            {{ item }}
                          </option>
                        </select>
                      </div>
                    </div>
                    <div class="modal-footer">
                      <button
                        type="button"
                        class="btn btn-primary"
                        data-bs-dismiss="modal"
                        aria-label="Close"
                      >
                        Cancel
                      </button>
                      <button class="btn btn-primary">Save changes</button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row row-cols-1 g-3">
          <div
            class="col"
            v-for="category in budget.categories"
            :key="category"
          >
            <div class="card">
              <div class="card-header">{{ category }}</div>
              <ul class="list-group list-group-flush">
                <li
                  class="list-group-item"
                  v-for="(item, idx) in budget.items.filter(
                    (item) => item.category === category
                  )"
                  :key="idx"
                >
                  <div class="row">
                    <div class="col">
                      <h6>{{ category }}</h6>
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
              <div class="card-footer">
                <button
                  type="button"
                  class="btn btn-primary"
                  @click="
                    budget.items.push({
                      name: '',
                      category,
                      plannedAmount: 0
                    })
                  "
                >
                  Add {{ category }}
                </button>
              </div>
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
      newTransaction: {
        type: "",
        date: "",
        vendor: "",
        item: "",
        amount: "",
        number: "",
        notes: ""
      },
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
        items: [{ name: "Income", category: "Income", plannedAmount: 2000 }]
      }
    };
  },
  methods: {
    addTranzaction() {
      this.budget.transactions.push(this.newTransaction);
      this.newTransaction = {
        type: "",
        date: "",
        vendor: "",
        item: "",
        amount: "",
        number: "",
        notes: ""
      };
    },
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
    itemNames() {
      return this.budget.items.map((item) => item.name);
    },
    incomeTotal() {
      return this.budget.transactions
        .filter((transaction) => {
          return transaction.category === "Income";
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
          return transaction.category !== "Income";
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

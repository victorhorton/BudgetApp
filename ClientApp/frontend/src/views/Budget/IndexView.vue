<template>
  <div class="row"></div>
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
        month: "",
        categories: []
      }
    };
  },
  mounted() {
    fetch("/api/Budget")
      .then((resp) => {
        return resp.json();
      })
      .then((data) => {
        debugger;
        this.budget = data;
      });
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

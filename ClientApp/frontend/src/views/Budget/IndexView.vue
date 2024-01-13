<template>
  <div class="row">
    <div class="col">
      <div class="container">
        <div class="row">
          <div class="col">
            <h1>{{ budget.month }}</h1>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-for="category in budget.categories" :key="category.id" class="row">
    <div class="col">
      <div class="card">
        <div class="card-body">
          <h5 class="card-title">
            <input
              type="text"
              class="form-control"
              :class="category.isValid ? 'is-valid' : ''"
              v-model="category.name"
              @change="updateCategory(category)"
            />
            <div class="invalid-feedback">
              {{ category.error }}
            </div>
          </h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional
            content.
          </p>
          <a href="#" class="btn btn-primary">Go somewhere</a>
        </div>
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col">
      <button type="button" class="btn btn-primary" @click="addCategory()">
        Add Category
      </button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "BudgetIndexView",
  data() {
    return {
      totalRemainingView: true,
      newCategory: {
        name: "",
        budgetId: null
      },
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
    axios.get("/api/Budget").then((resp) => {
      this.budget = resp.data;
      this.newCategory.budgetId = this.budget.id;
    });
  },
  methods: {
    addCategory() {
      axios
        .post("/api/category", {
          name: "",
          budgetId: this.budget.id
        })
        .then((response) => {
          if (response.status === 201) {
            this.budget.categories.push(response.data);
          }
          console.log(response);
        });
    },
    updateCategory(category) {
      delete category.error;
      axios.put(`/api/category/${category.id}`, category).catch((error) => {
        category.error = error.message;
      });
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

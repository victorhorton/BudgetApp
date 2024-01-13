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
  <div
    v-for="category in budget.categories"
    :key="category.id"
    class="row my-3"
  >
    <div class="col">
      <div class="card">
        <div class="card-body">
          <div class="row">
            <div class="col">
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
            </div>
          </div>
          <div class="row my-3">
            <div class="col">
              <ul class="list-group list-group-flush">
                <li
                  class="list-group-item"
                  v-for="item in category.items"
                  :key="item.id"
                >
                  <div class="row">
                    <div class="col">
                      <input
                        type="text"
                        class="form-control"
                        v-model="item.name"
                        @change="updateItem(item)"
                      />
                    </div>
                    <div class="col">
                      <input
                        type="number"
                        class="form-control"
                        v-model.number="item.plannedAmount"
                        @change="updateItem(item)"
                      />
                    </div>
                    <div class="col-auto">
                      <button
                        class="btn-close"
                        @click="deleteItem(item, category)"
                      ></button>
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
          <div class="row justify-content-between">
            <div class="col-auto">
              <button
                type="button"
                class="btn btn-link"
                @click="addItem(category)"
              >
                Add Item
              </button>
            </div>
            <div class="col-auto">
              <button
                type="button"
                class="btn btn-link"
                @click="deleteCategory(category)"
              >
                Delete Category
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col-auto">
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
    addItem(category) {
      axios
        .post("/api/item", {
          name: "",
          plannedAmount: 0,
          categoryId: category.id
        })
        .then((response) => {
          if (response.status === 201) {
            category.items.push(response.data);
          }
        });
    },
    deleteCategory(category) {
      axios.delete(`/api/category/${category.id}`).then((response) => {
        if (response.status === 204) {
          this.budget.categories.splice(
            this.budget.categories.indexOf(category),
            1
          );
        }
      });
    },
    deleteItem(item, category) {
      axios.delete(`/api/item/${item.id}`).then((response) => {
        if (response.status === 204) {
          category.items.splice(category.items.indexOf(item), 1);
        }
      });
    },
    updateItem(item) {
      delete item.error;
      axios.put(`/api/item/${item.id}`, item).catch((error) => {
        item.error = error.message;
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

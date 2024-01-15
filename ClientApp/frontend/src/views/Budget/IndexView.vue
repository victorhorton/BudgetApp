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
  <div class="row">
    <div class="col-8">
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
    </div>
    <div class="col-4">
      <ul class="list-group list-group-flush">
        <template v-for="transaction in transactions" :key="transaction.id">
          <li class="list-group-item list-group-item-action">
            <div class="row">
              <div class="col">
                <div
                  class="row pe-click"
                  data-bs-toggle="modal"
                  :data-bs-target="`#edit-transaction-${transaction.id}-modal`"
                >
                  <div class="col">{{ transaction.date }}</div>
                  <div class="col">{{ transaction.vendor }}</div>
                  <div class="col">{{ transaction.amount }}</div>
                </div>
              </div>
              <div class="col-auto">
                <button
                  class="btn-close"
                  @click="deleteTransactions(transaction)"
                ></button>
              </div>
            </div>
          </li>
          <div
            class="modal fade"
            :id="`edit-transaction-${transaction.id}-modal`"
            tabindex="-1"
            :aria-labelledby="`edit-transaction-${transaction.id}-modal-label`"
            aria-hidden="true"
          >
            <div class="modal-dialog modal-lg">
              <div class="modal-content">
                <div class="modal-header">
                  <h1
                    class="modal-title fs-5"
                    :id="`edit-transaction-${transaction.id}-modal-label`"
                  >
                    Edit Transaction
                  </h1>
                  <button
                    type="button"
                    class="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <form @submit.prevent="updateTransaction(transaction)">
                  <div class="modal-body">
                    <div class="row my-2">
                      <div class="col-auto">
                        <div class="form-check">
                          <input
                            class="form-check-input"
                            type="radio"
                            :id="`incomeRadio-${transaction.id}`"
                            value="income"
                            v-model="transaction.type"
                          />
                          <label
                            class="form-check-label"
                            :for="`incomeRadio-${transaction.id}`"
                          >
                            Income
                          </label>
                        </div>
                      </div>
                      <div class="col-auto">
                        <div class="form-check">
                          <input
                            class="form-check-input"
                            type="radio"
                            :id="`expenseRadio-${transaction.id}`"
                            value="expense"
                            v-model="transaction.type"
                          />
                          <label
                            class="form-check-label"
                            :for="`expenseRadio-${transaction.id}`"
                          >
                            Expense
                          </label>
                        </div>
                      </div>
                    </div>
                    <div class="row my-2">
                      <div class="col">
                        <input
                          type="number"
                          class="form-control"
                          v-model.number="transaction.amount"
                        />
                      </div>
                    </div>
                    <div class="row my-2">
                      <div class="col-auto">
                        <input
                          type="date"
                          class="form-control"
                          v-model="transaction.date"
                        />
                      </div>
                      <div class="col">
                        <input
                          type="text"
                          class="form-control"
                          v-model="transaction.vendor"
                        />
                      </div>
                    </div>
                    <div
                      class="row my-2"
                      v-for="itemTransaction in transaction.itemTransactions"
                      :key="itemTransaction.itemId"
                    >
                      <div class="col">
                        {{ itemName(itemTransaction.itemId) }}
                      </div>
                      <div class="col-auto">
                        <button
                          type="button"
                          class="btn-close"
                          @click="
                            deleteRelationship(
                              itemTransaction.itemId,
                              transaction
                            )
                          "
                        ></button>
                      </div>
                    </div>
                    <div class="row my-2">
                      <div class="col">
                        <select
                          v-model="transaction.newItemIds"
                          multiple
                          class="form-select"
                        >
                          <option
                            v-for="item in filteredItems(transaction)"
                            :key="item.id"
                            :value="item.id"
                          >
                            {{ item.name }}
                          </option>
                        </select>
                      </div>
                    </div>
                  </div>
                  <div class="modal-footer">
                    <button class="btn btn-primary">Save changes</button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </template>
      </ul>
    </div>
  </div>
  <div class="row justify-content-between">
    <div class="col-auto">
      <button type="button" class="btn btn-primary" @click="addCategory()">
        Add Category
      </button>
    </div>
    <div class="col-auto">
      <button
        type="button"
        class="btn btn-primary"
        data-bs-toggle="modal"
        data-bs-target="#transactionModal"
      >
        Add Transaction
      </button>
    </div>
  </div>
  <div
    class="modal fade"
    id="transactionModal"
    tabindex="-1"
    aria-labelledby="transactionModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="transactionModalLabel">
            Add Transaction
          </h1>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <form @submit.prevent="addTransaction()">
          <div class="modal-body">
            <div class="row">
              <div class="col-auto">
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="radio"
                    id="incomeRadio"
                    value="income"
                    v-model="newTransaction.type"
                  />
                  <label class="form-check-label" for="incomeRadio">
                    Income
                  </label>
                </div>
              </div>
              <div class="col-auto">
                <div class="form-check">
                  <input
                    class="form-check-input"
                    type="radio"
                    id="expenseRadio"
                    value="expense"
                    v-model="newTransaction.type"
                  />
                  <label class="form-check-label" for="expenseRadio">
                    Expense
                  </label>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col">
                <input
                  type="number"
                  class="form-control"
                  v-model.number="newTransaction.amount"
                />
              </div>
            </div>
            <div class="row">
              <div class="col-auto">
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
              <div class="col">
                <select
                  v-model="newTransaction.newItemIds"
                  multiple
                  class="form-select"
                >
                  <option v-for="item in items" :key="item.id" :value="item.id">
                    {{ item.name }}
                  </option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button class="btn btn-primary">Save Transaction</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { Modal } from "bootstrap";

export default {
  name: "BudgetIndexView",
  data() {
    return {
      newTransaction: {
        type: "income",
        date: "",
        vendor: "",
        amount: 0,
        number: "",
        notes: "",
        newItemIds: []
      },
      budget: {
        month: "",
        categories: []
      },
      transactions: []
    };
  },
  mounted() {
    axios.get("/api/Budget").then((resp) => {
      this.budget = resp.data;
    });
    axios.get("/api/Transaction").then((resp) => {
      this.transactions = resp.data;
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
    addTransaction() {
      axios.post("/api/transaction", this.newTransaction).then((response) => {
        if (response.status === 201) {
          this.transactions.push(response.data);
          const openedModal = Modal.getInstance(
            document.getElementById("transactionModal")
          );
          openedModal.hide();
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
    deleteRelationship(itemId, transaction) {
      axios
        .delete("/api/transaction/delete-relationship", {
          data: {
            itemId,
            transactionId: transaction.id
          }
        })
        .then(() => {
          const itemTransaction = transaction.itemTransactions.find(
            (itemTransaction) => {
              return itemTransaction.itemId === itemId;
            }
          );
          transaction.itemTransactions.splice(
            transaction.itemTransactions.indexOf(itemTransaction),
            1
          );
        });
    },
    deleteTransactions(transaction) {
      axios.delete(`/api/transaction/${transaction.id}`).then((response) => {
        if (response.status === 204) {
          this.transactions.splice(this.transactions.indexOf(transaction), 1);
        }
      });
    },
    itemName(itemId) {
      const foundItem = this.items.find((item) => {
        return item.id === itemId;
      });

      if (foundItem) {
        return foundItem.name;
      } else {
        return "";
      }
    },
    filteredItems(transaction) {
      const currentItems = transaction.itemTransactions.map(
        (itemTransaction) => {
          return itemTransaction.itemId;
        }
      );
      return this.items.filter((item) => {
        return !currentItems.includes(item.id);
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
    },
    updateTransaction(transaction) {
      axios
        .put(`/api/transaction/${transaction.id}`, transaction)
        .then(() => {
          transaction.newItemIds.forEach((newItemId) => {
            transaction.itemTransactions.push({
              item: null,
              itemId: newItemId,
              transactionId: transaction.id
            });
          });
          transaction.newItemIds = [];
          const openedModal = Modal.getInstance(
            document.getElementById(`edit-transaction-${transaction.id}-modal`)
          );
          openedModal.hide();
        })
        .catch((error) => {
          transaction.error = error.message;
        });
    }
  },
  computed: {
    items() {
      return this.budget.categories
        .map((category) => {
          return category.items.map((item) => {
            return { id: item.id, name: item.name };
          });
        })
        .flat();
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

<style>
.pe-click {
  cursor: pointer;
}
</style>

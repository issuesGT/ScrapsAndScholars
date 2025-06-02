import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import FoodPage from '../views/FoodPage.vue'
import LoginPage from '../views/LoginPage.vue'
import AddFood from '../views/AddFood.vue'
import FoodDetails from '../views/FoodDetails.vue'
import ClaimFood from '../views/ClaimFood.vue'
import Chat from '../views/Communication.vue'

const routes = [
  { path: '/', component: HomePage },
  { path: '/login', component: LoginPage },
  { path: '/food', component: FoodPage },
  { path: '/Addfood', component: AddFood },
  { path: '/foods/:id', name: 'FoodDetails', component: FoodDetails, props: true },
  { path: '/foods/claim/:id', name: 'ClaimFood', component: ClaimFood, props: true },
  { path: '/chatroom', component: Chat}
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router

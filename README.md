# 🛒 E-Commerce API (.NET Core)

This is a full-featured E-Commerce REST API built using **ASP.NET Core**. It supports functionalities for **Admin**, **Customer**, **Product**, **Cart**, and **Category** operations. Designed with scalability and modularity in mind, this backend API handles user authentication, product browsing, cart management, and more.

---

## 🚀 Technologies Used

- ASP.NET Core
- Entity Framework Core (EF Core)
- MSSQL Server
- RESTful API
- Swagger (for API documentation)

---

## 📌 Features

- ✅ Customer Signup, Login, Logout
- ✅ Admin Login
- ✅ Add, Edit, View, and Delete Products & Categories
- ✅ Search by Product Name or Category
- ✅ Add Products to Cart
- ✅ Calculate Total Price
- ✅ Checkout and Buy Products
- ✅ JWT Authentication Ready (if implemented)
- ✅ Clean Routing and Code Organization

---

## 📂 API Endpoints

### 👤 Admin
- `POST /admin/login/{email}/{password}` – Admin login

### 👥 Customer
- `POST /customer/signup` – Sign up
- `POST /customer/login/{email}/{password}` – Login
- `DELETE /customer/logout/{customerId}` – Logout

### 🛒 Cart
- `POST /cart/create-cart/{customerId}` – Create a cart
- `POST /cart/add-product-to-cart/{customerId}/{productId}/{quantity}` – Add product to cart
- `DELETE /cart/delete-from-cart/{customerId}/{productId}` – Remove product from cart
- `DELETE /cart/delete-car/{customerId}` – Delete all items from cart
- `GET /cart/calculate-total/{customerId}` – Get total cart price
- `DELETE /cart/checkout/{customerId}` – Checkout cart
- `DELETE /cart/buy-product/{customerId}/{productId}` – Buy a single product

### 📦 Product
- `GET /product/get-products/{categoryId}` – Products by category
- `GET /product/get-product/{id}` – Product details by ID
- `GET /product/get-product/{name}` – Product details by name
- `POST /product/add-product` – Add new product
- `PUT /product/edit-product/{productId}` – Edit product
- `DELETE /product/remove-product/{productId}` – Remove product

### 📝 Product Details
- `GET /details/get-product-name/{productId}`
- `GET /details/get-product-description/{productId}`
- `GET /details/get-product-price/{productId}`
- `GET /details/get-stock-quantity/{productId}`
- `GET /details/get-quantity-sold/{productId}`
- `GET /details/get-category-name/{productId}`
- `GET /details/show-image/{productId}`

### 🗂️ Category
- `GET /category/get-categories` – All categories
- `GET /category/get-categories-with-products` – Categories with their products
- `GET /category/category-details/{id}` – Category details by ID
- `GET /category/category-details/{name}` – Category details by name
- `POST /category/add-category` – Add new category
- `PUT /category/edit-category-name/{id}/{name}` – Edit category name
- `DELETE /category/remove-category/{id}` – Remove category

---

## 🧠 How It Works

1. Admin or customer authenticates with credentials.
2. Customers can browse products, add them to cart, calculate total, and checkout.
3. Admin can manage product listings and categories.
4. All routes are RESTful and organized by controller.

---

## ⚙️ Getting Started

### Prerequisites
- .NET 6 or later
- MSSQL Server
- Visual Studio or VS Code

### Setup

```bash
git clone https://github.com/your-username/ecommerce-dotnet-api.git
cd ecommerce-dotnet-api

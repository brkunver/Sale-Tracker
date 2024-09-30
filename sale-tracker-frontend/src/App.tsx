import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { ReactQueryDevtools } from "@tanstack/react-query-devtools"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import { authLoader } from "./utils/authLoader"

// Import Pages
import LoginPage from "./pages/LoginPage"
import DashboardPage from "./pages/DashboardPage"
import NotFound from "./pages/NotFound"

// import product pages
import ProductsPage from "./pages/ProductPages/ProductsPage"
import SingleProductPage from "./pages/ProductPages/SingleProductPage"
import SalesPage from "./pages/SalePages/SalesPage"
import CustomersPage from "./pages/CustomerPages/CustomersPage"
import EditProductPage from "./pages/ProductPages/EditProductPage"
import SearchProductsPage from "./pages/ProductPages/SearchProductsPage"
import AddProductPage from "./pages/ProductPages/AddProductPage"

// import customer pages
import AddCustomer from "./components/customers/AddCustomer"
import EditCustomerPage from "./pages/CustomerPages/EditCustomerPage"
import SinglecustomerPage from "./pages/CustomerPages/SingleCustomerPage"
import AddCustomerPage from "./pages/CustomerPages/AddCustomerPage"
import SearchCustomersPage from "./pages/CustomerPages/SearchCustomersPage"
// query client
const queryClient = new QueryClient({
  defaultOptions: { queries: { gcTime: 1000 * 60 * 2, staleTime: 1000 * 60 * 2 } },
})
// routes
const router = createBrowserRouter([
  {
    path: "/",
    element: <DashboardPage />,
    loader: authLoader,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/dashboard",
    element: <DashboardPage />,
    loader: authLoader,
  },
  {
    path: "/products",
    element: <ProductsPage />,
    loader: authLoader,
  },
  {
    path: "/search-products",
    element: <SearchProductsPage />,
    loader: authLoader,
  },
  {
    path: "/product/:id",
    element: <SingleProductPage />,
    loader: authLoader,
  },
  {
    path: "/edit-product/:id",
    element: <EditProductPage />,
    loader: authLoader,
  },
  {
    path: "/add-product",
    element: <AddProductPage />,
    loader: authLoader,
  },
  {
    path: "/sales",
    element: <SalesPage />,
    loader: authLoader,
  },
  {
    path: "/customers",
    element: <CustomersPage />,
    loader: authLoader,
  },
  {
    path: "/edit-customer/:id",
    element: <EditCustomerPage />,
    loader: authLoader,
  },
  {
    path: "/add-customer",
    element: <AddCustomerPage />,
    loader: authLoader,
  },
  {
    path: "/search-customers",
    element: <SearchCustomersPage />,
    loader: authLoader,
  },
  {
    path: "/customer/:id",
    element: <SinglecustomerPage />,
    loader: authLoader,
  },
  {
    path: "*",
    element: <NotFound />,
  },
])

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} />
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  )
}

export default App

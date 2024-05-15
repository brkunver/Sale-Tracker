import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { ReactQueryDevtools } from "@tanstack/react-query-devtools"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import { authLoader } from "./utils/authLoader"
// Import Pages
import LoginPage from "./pages/LoginPage"
import DashboardPage from "./pages/DashboardPage"
import NotFound from "./pages/NotFound"
import ProductsPage from "./pages/ProductsPage"
import SingleProductPage from "./pages/SingleProductPage"
import SalesPage from "./pages/SalesPage"
import CustomersPage from "./pages/CustomersPage"
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
    path: "/product/:id",
    element: <SingleProductPage />,
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

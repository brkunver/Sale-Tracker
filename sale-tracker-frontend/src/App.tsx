import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import { authLoader } from "./utils/authLoader"
import DashboardPage from "./pages/DashboardPage"
import NotFound from "./pages/NotFound"
import ProductsPage from "./pages/ProductsPage"
import SalesPage from "./pages/SalesPage"

const queryClient = new QueryClient()
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
    path: "/sales",
    element: <SalesPage />,
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
    </QueryClientProvider>
  )
}

export default App
import SideBar from "@/components/SideBar"
import AddProduct from "@/components/products/AddProduct"

export default function AddProductPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex-col flex items-center">
        <h1 className="font-bold text-3xl my-4">Add Product</h1>
        <AddProduct />
      </main>
    </div>
  )
}

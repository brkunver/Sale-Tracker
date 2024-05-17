import SideBar from "@/components/SideBar"
import { SearchProducts } from "@/components/products/SearchProducts"

export default function EditProductPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto flex-col flex items-center">
        <h1 className="font-bold text-3xl my-4">Edit Product</h1>
        <SearchProducts />
      </main>
    </div>
  )
}

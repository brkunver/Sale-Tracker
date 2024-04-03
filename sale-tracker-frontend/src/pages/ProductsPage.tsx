import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"

function ProductsPage() {
  return (
    <div className="flex">
      <SideBar />
      <main className="flex flex-col">
        <h1>Products</h1>
        <div className="grid lg:grid-cols-2">
          <Products className="" page={1} count={3} />
        </div>
      </main>
    </div>
  )
}

export default ProductsPage

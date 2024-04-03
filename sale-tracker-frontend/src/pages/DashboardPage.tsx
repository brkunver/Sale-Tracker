import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import Sales from "@/components/sales/Sales"

function DashboardPage() {
  return (
    <div className="flex">
      <SideBar />
      <main className="flex flex-col items-center text-center mx-auto">
        <h1 className="text-3xl ">Dashboard</h1>
        <div className="grid grid-cols-2 gap-10">
          <Products />
          <Sales />
        </div>
      </main>
    </div>
  )
}

export default DashboardPage

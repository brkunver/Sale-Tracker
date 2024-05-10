import SideBar from "@/components/SideBar"
import Products from "@/components/products/Products"
import Sales from "@/components/sales/Sales"
import LastSalesChart from "@/components/sales/LastSalesChart"
import { RefreshCcw } from "lucide-react"

import { useState } from "react"
//TODO : add shadcn day picker for last sales day
function DashboardPage() {
  const [reloads, setReloads] = useState({
    reloadProducts: () => null,
    reloadSales: () => null,
  })
  const [lastSalesDay, setLastSalesDay] = useState(20)

  async function reloadHandler() {
    reloads.reloadProducts()
    reloads.reloadSales()
  }

  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="flex flex-col items-center text-center mx-auto w-full lg:px-10">
        <h1 className="text-3xl lg:py-8 font-sans">Dashboard</h1>
        <button
          onClick={reloadHandler}
          className="flex justify-center items-center text-lg font-semibold rounded bg-gray-200 px-2 py-1 gap-2 mb-4 hover:outline active:scale-90"
        >
          <span>Reload All</span>
          <RefreshCcw size={24} className="text-green-700" />
        </button>
        <h2 className="text-lg font-sans">
          Revenue of the last <span className="font-bold italic">{lastSalesDay}</span> sales
        </h2>
        <section className="mx-auto">
          <LastSalesChart day={lastSalesDay} />
        </section>
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-10 lg:max-w-[50rem] w-full my-10">
          <Products className="" setRefetch={setReloads} />
          <Sales className="" setRefetch={setReloads} />
        </div>
      </main>
    </div>
  )
}

export default DashboardPage

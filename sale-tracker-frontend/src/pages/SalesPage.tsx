import SideBar from "@/components/SideBar"
import { getLastSales } from "@/utils/saleApiCalls"

function SalesPage() {
  
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="flex flex-col">
        <h1>Sales</h1>
      </main>
    </div>
  )
}

export default SalesPage

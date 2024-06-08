import SideBar from "@/components/SideBar"
import Sales from "@/components/sales/Sales"
import { useState } from "react"

function SalesPage() {
  const [page, setPage] = useState(1)
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="grid grid-cols-2">
        <Sales count={10} page={1} />
      </main>
    </div>
  )
}

export default SalesPage

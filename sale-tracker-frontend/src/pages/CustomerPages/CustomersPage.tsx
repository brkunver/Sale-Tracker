import SideBar from "@/components/SideBar"
import Customers from "@/components/customers/Customers"
import { useState } from "react"

export default function CustomersPage() {
  const [page, setPage] = useState(1)
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="mx-auto">
        <h1 className="text-center font-bold text-2xl">Customers</h1>
        <Customers showDelete/>
      </main>
    </div>
  )
}

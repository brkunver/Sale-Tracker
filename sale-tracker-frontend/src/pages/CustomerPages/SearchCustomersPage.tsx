import SideBar from "@/components/SideBar"
import SearchCustomer from "@/components/customers/SearchCustomer"
function SearchCustomersPage() {
  return (
    <div className="flex min-h-screen">
      <SideBar />
      <main className="flex flex-col mx-auto items-center">
        <h1 className="text-center font-bold text-2xl mb-4">Search Customers</h1>
        <SearchCustomer />
      </main>
    </div>
  )
}

export default SearchCustomersPage

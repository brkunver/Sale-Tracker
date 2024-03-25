import { BadgeDollarSign, Banknote, LogOut, ShoppingBag } from "lucide-react"
import { Link } from "react-router-dom"

function SideBar() {
  return (
    <aside className="w-[80px] lg:w-[200px] h-screen flex flex-col bg-gray-900 text-white items-center">
      <div className="hidden lg:flex flex-col text-lg font-bold gap-10 pt-6">
        <Link
          to={"/dashboard"}
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110 "
        >
          <span>Sale Tracker</span>
          <Banknote size={30} color="#03c700" />
        </Link>
        <Link
          to={"/products"}
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110"
        >
          <span>Products</span>
          <ShoppingBag size={30} />
        </Link>
        <Link to={"/sales"} className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110">
          <span>Sales</span>
          <BadgeDollarSign size={30} />
        </Link>
        <button
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110"
          onClick={() => {
            localStorage.removeItem("token")
            window.location.reload()
          }}
        >
          <span>Log out</span>
          <LogOut size={30} />
        </button>
      </div>

      <div className="lg:hidden flex flex-col gap-6">
        <Banknote size={40} color="#03c700" />
        <Link to={"/products"}>
          <ShoppingBag size={40} />
        </Link>
        <Link to={"/sales"}>
          <BadgeDollarSign size={40} />
        </Link>

        <button
          className=""
          onClick={() => {
            localStorage.removeItem("token")
            window.location.reload()
          }}
        >
          <LogOut size={40} />
        </button>
      </div>
    </aside>
  )
}

export default SideBar

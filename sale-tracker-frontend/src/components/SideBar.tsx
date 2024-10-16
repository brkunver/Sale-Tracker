import localforage from "localforage"
import { BadgeDollarSign, Banknote, LogOut, ShoppingBag, CircleUserRoundIcon } from "lucide-react"
import React from "react"
import { Link, redirect } from "react-router-dom"

function SideBar() {

  function logoutHandler() {
    localStorage.removeItem("token")
    localforage.setItem("demo", false).then(() => {
      redirect("/login")
      window.location.reload()
    }).catch(err => {
      console.log(err)
    })
  }

  return (
    <aside className="w-[80px] lg:min-w-[200px] min-h-full flex flex-col bg-gray-900 text-white items-center">
      <div className="hidden lg:flex flex-col text-lg font-bold gap-10 pt-6">
        <Link
          to={"/dashboard"}
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110 text-nowrap"
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
        <Link
          to={"/customers"}
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110"
        >
          <span>Customers</span>
          <CircleUserRoundIcon size={30} />
        </Link>
        <button
          className="flex justify-center items-center gap-2 duration-100 ease-in-out hover:scale-110"
          onClick={logoutHandler}
        >
          <span>Log out</span>
          <LogOut size={30} />
        </button>
      </div>

      <div className="lg:hidden flex flex-col gap-6">
        <Link to={"/dashboard"}>
          <Banknote size={40} color="#03c700" />
        </Link>
        <Link to={"/products"}>
          <ShoppingBag size={40} />
        </Link>
        <Link to={"/sales"}>
          <BadgeDollarSign size={40} />
        </Link>

        <Link to={"/customers"}>
          <CircleUserRoundIcon size={40} />
        </Link>

        <button id="log-out-button" onClick={logoutHandler}>
          <LogOut size={40} />
        </button>
      </div>
    </aside>
  )
}

export default React.memo(SideBar)

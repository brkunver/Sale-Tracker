import localforage from "localforage"
import { redirect } from "react-router-dom"

export async function authLoader() {
  const demo = await localforage.getItem("demo") 
  if (demo === true) {
    return null
  }

  const token = localStorage.getItem("token")
  if (token === null || token === "") {
    return redirect("/login")
  } else {
    try {
      let response = await fetch(import.meta.env.VITE_API_URL + "/health", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      if (response.ok) {
        return null
      }
    } catch (error) {
      return redirect("/login")
    }
    return redirect("/login")
  }
}

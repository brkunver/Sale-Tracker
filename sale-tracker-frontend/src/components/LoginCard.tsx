import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { useState } from "react"
import { useNavigate } from "react-router-dom"

function LoginCard() {
  const [fetchState, setFetchState] = useState({
    isLoading: false,
    isError: false,
  })
  const [formInfo, setFormInfo] = useState({
    username: "",
    password: "",
  })

  let navigate = useNavigate()

  async function onSubmit(e: any) {
    e.preventDefault()
    setFetchState((prev) => ({ ...prev, isLoading: true, isError: false }))
    try {
      let response = await fetch(import.meta.env.VITE_API_URL + "/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formInfo),
      })
      if (!response.ok) {
        setFetchState((prev) => ({ ...prev, isError: true }))
      } else {
        let data = await response.json()
        localStorage.setItem("token", data.token)
        navigate("/dashboard")
      }
    } catch (error) {
      setFetchState((prev) => ({ ...prev, isError: true }))
    }

    setFetchState((prev) => ({ ...prev, isLoading: false }))
  }

  function handleInputs(e: React.ChangeEvent<HTMLInputElement>) {
    setFormInfo((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }

  console.log(formInfo)
  return (
    <form onSubmit={onSubmit} className="flex flex-col w-96 rounded border mx-auto p-6 gap-6 ">
      <h1 className="text-center text-3xl font-bold">Login</h1>
      <div className="flex items-center gap-4">
        <Label htmlFor="username" className="min-w-24">
          Username
        </Label>
        <Input id="username" name="username" type="text" autoFocus onChange={handleInputs} />
      </div>
      <div className="flex items-center gap-4">
        <Label htmlFor="password" className="min-w-24">
          Password
        </Label>
        <Input id="password" name="password" type="password" onChange={handleInputs} />
      </div>

      <Button type="submit">Login</Button>
      {fetchState.isLoading && <p className="text-center font-semibold">Loading...</p>}
      {fetchState.isError && <p className="text-center font-semibold text-red-600">Error : Can't Login In</p>}
    </form>
  )
}

export default LoginCard

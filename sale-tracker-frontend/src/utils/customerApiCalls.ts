import type { Customer, CustomerData } from "@/types/customerTypes"

function getTokenHeader() {
  let bearerToken = localStorage.getItem("token")
  let headers = new Headers()
  headers.append("Authorization", "Bearer " + bearerToken)
  return headers
}

export async function GetAllCustomer(page?: number, count?: number, returnDeleted?: boolean, name?: string) {
  let url = import.meta.env.VITE_API_URL + "/api/customer"
  let params = new URLSearchParams()
  if (page) params.append("page", page.toString())
  if (count) params.append("count", count.toString())
  if (returnDeleted) params.append("returnDeleted", returnDeleted.toString())
  if (name) params.append("name", name)

  if (params.toString()) url += "?" + params.toString()

  const response = await fetch(url, { headers: getTokenHeader() })
  if (!response.ok) throw new Error(response.statusText)
  const data = await response.json()
  return data.data as Customer[]
}

export async function GetSingleCustomer(customerId: number) {
  let url = import.meta.env.VITE_API_URL + "/api/customer/" + customerId
  let response = await fetch(url, { headers: getTokenHeader() })
  if (!response.ok) throw new Error(response.statusText)
  let data = await response.json()
  return data as Customer
}

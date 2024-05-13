import type { Customer, CustomerData } from "@/types/customerTypes"

import getTokenHeader from "./getTokenHeader"

export async function getAllCustomers(page?: number, count?: number, returnDeleted?: boolean, name?: string) {
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

export async function getSingleCustomer(customerId: number) {
  let url = import.meta.env.VITE_API_URL + "/api/customer/" + customerId
  let response = await fetch(url, { headers: getTokenHeader() })
  if (!response.ok) throw new Error(response.statusText)
  let data = await response.json()
  return data as Customer
}
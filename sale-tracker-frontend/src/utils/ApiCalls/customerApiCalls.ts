import type { Customer } from "@/types/customerTypes"

import getTokenHeader from "./getTokenHeader"

interface getAllCustomersQuery {
  page?: number
  count?: number
  returnDeleted?: boolean
  name?: string
}

export async function getAllCustomers({ page, count, returnDeleted, name }: getAllCustomersQuery) {
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

export async function getSingleCustomer(customerId: string) {
  let url = import.meta.env.VITE_API_URL + "/api/customer/" + customerId
  let response = await fetch(url, { headers: getTokenHeader() })
  if (!response.ok) throw new Error(response.statusText)
  const data = await response.json()
  return data.data as Customer
}

export async function addNewCustomer(formData: FormData) {
  let url = import.meta.env.VITE_API_URL + "/api/customer"
  let response = await fetch(url, {
    method: "POST",
    headers: getTokenHeader(),
    body: formData,
  })
  if (!response.ok) throw new Error(response.statusText)
  const data = await response.json()
  return data.data as Customer
}

export async function updateCustomer(customerId: string, formData: FormData) {
  let url = import.meta.env.VITE_API_URL + "/api/customer/" + customerId
  let response = await fetch(url, {
    method: "PUT",
    headers: getTokenHeader(),
    body: formData,
  })

  const data = await response.json()
  console.log(data)
  return data.data as Customer
}

export async function deleteCustomer(customerId: string) {
  let url = import.meta.env.VITE_API_URL + "/api/customer/" + customerId
  let response = await fetch(url, {
    method: "DELETE",
    headers: getTokenHeader(),
  })
  if (!response.ok) throw new Error(response.statusText)
}

export async function getCustomerCount() {
  let url = import.meta.env.VITE_API_URL + "/api/customer/count"
  let response = await fetch(url, { headers: getTokenHeader() })
  if (!response.ok) throw new Error(response.statusText)
  const data = await response.json()
  return data.data as number
}

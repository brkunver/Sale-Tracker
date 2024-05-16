import type { Product, ProductData } from "@/types/productTypes"
import getTokenHeader from "./getTokenHeader"

enum queryNames {
  page = "page",
  count = "count",
  returnDeleted = "returnDeleted",
  name = "name",
}

type ProductQuery = {
  page?: number
  count?: number
  returnDeleted?: boolean
  name?: string
}

export async function getAllProducts(productQuery: ProductQuery = {}) {
  let url = import.meta.env.VITE_API_URL + "/api/product"
  let params = new URLSearchParams()

  if (productQuery.page) {
    params.append(queryNames.page, productQuery.page.toString())
  }
  if (productQuery.name) {
    params.append(queryNames.name, productQuery.name)
  }
  if (productQuery.count) {
    params.append(queryNames.count, productQuery.count.toString())
  }
  if (productQuery.returnDeleted) {
    params.append(queryNames.returnDeleted, "true")
  }
  url += "?" + params.toString()
  let response = await fetch(url, {
    headers: getTokenHeader(),
  })

  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data as ProductData
}

export function getImageUrl(imagename: string) {
  let url = import.meta.env.VITE_API_URL + "/uploads/" + imagename
  return url
}

export async function getSingleProduct(productId: string) {
  let url = import.meta.env.VITE_API_URL + "/api/product/" + productId
  let response = await fetch(url, {
    headers: getTokenHeader(),
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data.data as Product
}

export async function getCount() {
  let url = import.meta.env.VITE_API_URL + "/api/product/count"
  let response = await fetch(url, {
    headers: getTokenHeader(),
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data.data as number
}

// delete product
export async function deleteProduct(productId: string) {
  let url = import.meta.env.VITE_API_URL + "/api/product/" + productId
  let response = await fetch(url, {
    method: "DELETE",
    headers: getTokenHeader(),
  })
  if (!response.ok) {
    let data = await response.json()
    throw new Error(data.message)
  }
}

// TODO : Update product
export async function updateProduct(productId: string, formData: FormData) {
  let url = import.meta.env.VITE_API_URL + "/api/product/" + productId
  let response = await fetch(url, {
    method: "PUT",
    headers: getTokenHeader(),
    body: formData,
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data
}

export async function addNewProduct(formData: FormData) {
  let url = import.meta.env.VITE_API_URL + "/api/product"
  let response = await fetch(url, {
    method: "POST",
    headers: getTokenHeader(),
    body: formData,
  })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  let data = await response.json()
  return data
}

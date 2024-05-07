import type { Product, ProductData } from "@/types/productTypes"

enum queryNames {
  page = "page",
  count = "count",
  returnDeleted = "returnDeleted",
}

function getTokenHeader() {
  let bearerToken = localStorage.getItem("token")
  let headers = new Headers()
  headers.append("Authorization", "Bearer " + bearerToken)
  return headers
}

export async function getAllProducts(page?: number, count?: number, returnDeleted?: boolean) {
  let url = import.meta.env.VITE_API_URL + "/api/product"
  let params = new URLSearchParams()

  if (page) {
    params.append(queryNames.page, page.toString())
  }
  if (count) {
    params.append(queryNames.count, count.toString())
  }
  if (returnDeleted) {
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

export async function getSingleProduct(productId: number) {
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

export async function getImageUrlById(productId: number) {
  let product = await getSingleProduct(productId)
  return getImageUrl(product.imageUrl)
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
export async function deleteProduct(productId: number) {
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
export async function updateProduct(productId: number, input: any) {
  let url = import.meta.env.VITE_API_URL + "/api/product/" + productId

  let formData = new FormData()
  formData.append("name", input.name)
  formData.append("description", input.description)
  formData.append("price", input.price.toString())
  if (input.imageFile) {
    formData.append("imageFile", input.imageFile)
  }
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

export async function AddNewProduct(formData: FormData) {
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

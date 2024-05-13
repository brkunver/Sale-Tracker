export type Customer = {
  id: string
  name: string
  phone: string
  address: string
  createdOn: string
  updatedOn: string
}

export type CustomerData = {
  success: boolean
  message: string
  data: Customer[]
}

import { useMutation, useQueryClient } from "@tanstack/react-query"
import { updateProduct } from "@/utils/ApiCalls/productApiCalls"
import { Product } from "@/types/productTypes"
import { Input } from "../ui/input"
import { Button } from "../ui/button"
import { useEffect } from "react"
import { useNavigate } from "react-router-dom"

interface Props {
  product: Product
}

export default function EditProduct(props: Props) {
  const redirect = useNavigate()
  const queryClient = useQueryClient()
  const mutation = useMutation({
    mutationFn: async (mutatedProduct: FormData) => {
      return await updateProduct(props.product.id, mutatedProduct)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] })
      queryClient.invalidateQueries({ queryKey: ["single-product", props.product.id] })
      redirect(`/product/${props.product.id}`)
      clearInputs()
    },
  })

  useEffect(() => {
    let productNameInput = document.getElementById("edit-name") as HTMLInputElement
    productNameInput.value = props.product.name

    let productDescInput = document.getElementById("edit-desc") as HTMLInputElement
    productDescInput.value = props.product.description

    let productPriceInput = document.getElementById("edit-price") as HTMLInputElement
    productPriceInput.value = props.product.price.toString()
  }, [])

  function formSubmitHandler(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    let formData = new FormData(event.currentTarget)
    formData.set("Price" , (formData.get("Price") as string).replace(".", ","))
    mutation.mutate(formData)
  }

  function clearInputs() {
    const inputs = document.querySelectorAll('input[type="text"], input[type="file"]')
    inputs.forEach((input) => ((input as HTMLInputElement).value = ""))
  }

  return (
    <form onSubmit={formSubmitHandler} className="mx-auto flex flex-col px-8 gap-2 border rounded py-10">
      <h2 className="text-center font-semibold text-lg mb-2">Editing Product</h2>
      <div className="grid lg:grid-cols-2">
        <span className="font-semibold">Select a jpg image or leave empty</span>
        <Input type="file" name="FormFile" accept="image/jpeg" />
      </div>
      <Input type="text" id="edit-name" required aria-required name="Name" placeholder="Product Name" />
      <Input type="text" id="edit-desc" required aria-required name="Description" placeholder="Description" />

      <Input
        type="text"
        id="edit-price"
        required
        aria-required
        pattern="^\d{1,10}([.,]\d{1,2})?$"
        name="Price"
        placeholder="Price, ex : 24,50"
      />

      <Button className="w-full mx-auto px-2" type="submit">
        Edit Product
      </Button>
    </form>
  )
}

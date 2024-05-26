import { useMutation, useQueryClient } from "@tanstack/react-query"
import { updateCustomer } from "@/utils/ApiCalls/customerApiCalls.ts"
import type { Customer } from "@/types/customerTypes.ts"
import { Input } from "../ui/input"
import { Button } from "../ui/button"
import { useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { Label } from "@/components/ui/label.tsx"

interface Props {
  customer: Customer
}

export default function Editcustomer(props: Props) {
  const redirect = useNavigate()
  const queryClient = useQueryClient()
  const mutation = useMutation({
    mutationFn: async (mutatedCustomer: FormData) => {
      return await updateCustomer(props.customer.id, mutatedCustomer)
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["customers"] })
      queryClient.invalidateQueries({ queryKey: ["single-customer", props.customer.id] })
      redirect(`/customer/${props.customer.id}`)
      clearInputs()
    },
  })

  useEffect(() => {
    let customerNameInput = document.getElementById("edit-name") as HTMLInputElement
    customerNameInput.value = props.customer.name

    let customerPhoneInput = document.getElementById("edit-phone") as HTMLInputElement
    customerPhoneInput.value = props.customer.phone

    let customerAdressInput = document.getElementById("edit-adress") as HTMLInputElement
    customerAdressInput.value = props.customer.address
  }, [props.customer])

  function formSubmitHandler(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    let formData = new FormData(event.currentTarget)
    mutation.mutate(formData)
  }

  function clearInputs() {
    const inputs = document.querySelectorAll('input[type="text"], input[type="file"]')
    inputs.forEach((input) => ((input as HTMLInputElement).value = ""))
  }

  if (mutation.isPending) {
    return <div>Editing Customer...</div>
  }

  return (
    <form onSubmit={formSubmitHandler} className="mx-auto flex flex-col px-8 gap-2 border rounded py-10">
      <h2 className="text-center font-semibold text-lg mb-2">Editing : {props.customer.name}</h2>
      <Label htmlFor="edit-name" className="text-base font-semibold text-center underline">
        Customer Name
      </Label>
      <Input type="text" id="edit-name" required aria-required name="Name" placeholder="Customer Name" />
      <Label htmlFor="edit-phone" className="text-base font-semibold text-center underline">
        Customer Phone
      </Label>
      <Input
        type="text"
        id="edit-phone"
        required
        aria-required
        name="Phone"
        placeholder="Customer Phone"
        pattern="^[0-9]{8,20}$"
      />
      <Label htmlFor="edit-adress" className="text-base font-semibold text-center underline">
        Customer Address
      </Label>
      <Input type="text" id="edit-adress" required aria-required name="Address" placeholder="Customer Adress" />
      <Button className="w-full mx-auto px-2" type="submit">
        Edit Customer
      </Button>
    </form>
  )
}

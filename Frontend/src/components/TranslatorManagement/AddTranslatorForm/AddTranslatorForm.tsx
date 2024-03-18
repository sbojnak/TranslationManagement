import { addTranslator } from "@/apiServices/translatorsService"
import { Translator } from "@/apiTypes/Translator"
import { Button } from "@/components/ui/button"
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"

interface AddTranslatorFormProps {
  onNewTranslatorAdded: () => void;
}

const AddTranslatorForm = ( { onNewTranslatorAdded }: AddTranslatorFormProps) => {

  const addNewTranslator = (translator: Translator) => {
    addTranslator(translator)
      .then(() => onNewTranslatorAdded());
  }
  
  const formSchema = z.object({
    name: z.string().min(1, {
      message: "Translator's name cannot be empty.",
    }),
    hourlyRate: z.coerce.number().min(0, {
      message: "Hourly rate has to be at least 0.",
    }),
    status: z.union([
      z.literal("Applicant"),
      z.literal("Certified"),
      z.literal("Deleted")
    ]) ,
    creditCardNumber: z.string(),
  })

  const form = useForm<Translator>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      hourlyRate: "0",
      status: "",
      creditCardNumber: "",
    },
  })
  
  return (
    <div className="grid gap-4 py-4">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(addNewTranslator)} className="space-y-8">
          <FormField
            control={form.control}
            name="name"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Name</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="hourlyRate"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Hourly rate</FormLabel>
                <FormControl>
                  <Input type="number" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="status"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Status</FormLabel>
                <FormControl>
                <Select onValueChange={field.onChange} defaultValue={field.value}>
                  <SelectTrigger className="w-[180px]">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="Applicant">Applicant</SelectItem>
                    <SelectItem value="Certified">Certified</SelectItem>
                    <SelectItem value="Deleted">Deleted</SelectItem>
                  </SelectContent>
                </Select>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="creditCardNumber"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Credit card number</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit">Submit</Button>
        </form>
      </Form>
    </div>
  )
}

export default AddTranslatorForm;
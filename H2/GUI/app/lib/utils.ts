import type { ClassValue } from "clsx"
import { clsx } from "clsx"
import { twMerge } from "tailwind-merge"

//Comes from shadcn
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

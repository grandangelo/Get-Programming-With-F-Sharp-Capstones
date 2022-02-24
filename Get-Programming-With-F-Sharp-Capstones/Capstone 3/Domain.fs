module Capstone3.Domain

type Customer = { Name: string }
type Account = { AccountID: System.Guid; Owner: Customer; Amount: decimal }

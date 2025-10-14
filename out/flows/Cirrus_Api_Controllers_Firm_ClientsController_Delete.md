[web] DELETE /api/clients/{id:Guid}  (Cirrus.Api.Controllers.Firm.ClientsController.Delete)  [L191–L197] [auth=user]
  └─ calls ClientRepository.WriteQuery [L194]
  └─ writes_to Client [L194]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L194]


[web] PUT /api/clients/{id:Guid}  (Cirrus.Api.Controllers.Firm.ClientsController.Update)  [L179–L186] [auth=user]
  └─ calls ClientRepository.WriteQuery [L182]
  └─ writes_to Client [L182]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L182]


[web] PUT /api/clients/{id:Guid}  (Cirrus.Api.Controllers.Firm.ClientsController.Update)  [L179–L186] status=200 [auth=user]
  └─ calls ClientRepository.WriteQuery [L182]
  └─ write Client [L182]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L182]
      └─ ... (no implementation details available)


[web] PUT /api/central/tenants/{dataverseId}  (Cirrus.Api.Controllers.Central.CentralController.ProcessCoreDataverseChanges)  [L47–L69] [auth=Authentication.MachineToMachinePolicy]
  └─ calls CentralRepository.Commit [L67]
  └─ calls CentralRepository.WriteTable [L50]
  └─ uses_service CentralRepository
    └─ method WriteTable [L50]


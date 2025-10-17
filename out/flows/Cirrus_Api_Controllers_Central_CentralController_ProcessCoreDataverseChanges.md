[web] PUT /api/central/tenants/{dataverseId}  (Cirrus.Api.Controllers.Central.CentralController.ProcessCoreDataverseChanges)  [L47–L69] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls CentralRepository (methods: Commit,WriteTable) [L67]
  └─ uses_service CentralRepository
    └─ method WriteTable [L50]
      └─ implementation Cirrus.Central.Data.CentralRepository.WriteTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository


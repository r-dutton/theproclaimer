[web] PUT /api/central/firms/{dataverseId}  (Cirrus.Api.Controllers.Central.FirmController.UpdateFirm)  [L94–L105] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls CentralRepository (methods: Commit,WriteTable) [L101]
  └─ uses_service CentralRepository
    └─ method WriteTable [L97]
      └─ implementation Cirrus.Central.Data.CentralRepository.WriteTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository


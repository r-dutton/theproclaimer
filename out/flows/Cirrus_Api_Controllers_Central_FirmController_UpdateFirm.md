[web] PUT /api/central/firms/{dataverseId}  (Cirrus.Api.Controllers.Central.FirmController.UpdateFirm)  [L94–L105] [auth=Authentication.MachineToMachinePolicy]
  └─ calls CentralRepository.Commit [L101]
  └─ calls CentralRepository.WriteTable [L97]
  └─ uses_service CentralRepository
    └─ method WriteTable [L97]


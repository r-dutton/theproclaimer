[web] POST /api/central/firms/{firmId}/owners/{centralUserId}/resend-invite  (Cirrus.Api.Controllers.Central.FirmController.ResendUserInvite)  [L107–L119] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ calls CentralRepository.ReadTable [L111]
  └─ uses_service CentralRepository
    └─ method ReadTable [L111]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository


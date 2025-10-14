[web] PUT /api/firms/firm-account-types  (Workpapers.Next.API.Controllers.Firms.FirmsController.FirmAccountTypes)  [L201–L214] [auth=AuthorizationPolicies.Administrator]
  └─ calls FirmRepository.WriteQuery [L207]
  └─ writes_to Firm [L207]
    └─ reads_from Firms
  └─ uses_service IControlledRepository<Firm>
    └─ method WriteQuery [L207]
  └─ uses_service UserService
    └─ method GetFirmId [L205]


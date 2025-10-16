[web] PUT /api/firms/firm-account-types  (Workpapers.Next.API.Controllers.Firms.FirmsController.FirmAccountTypes)  [L209–L222] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls FirmRepository.WriteQuery [L215]
  └─ write Firm [L215]
    └─ reads_from Firms
  └─ uses_service IControlledRepository<Firm>
    └─ method WriteQuery [L215]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L213]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]


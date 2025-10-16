[web] GET /api/stats/firm-preferences  (Workpapers.Next.API.Controllers.StatsController.FirmPreferences)  [L252–L263] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=FirmPreferences) [L255]
  └─ sends_request GetFirmPreferencesStatsQuery [L259]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmPreferencesStatsQueryHandler.Handle [L39–L213]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method ReadQuery [L136]
          └─ ... (no implementation details available)
      └─ uses_service List<FirmPreferenceStat>
        └─ method Add [L174]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L68]
          └─ ... (no implementation details available)


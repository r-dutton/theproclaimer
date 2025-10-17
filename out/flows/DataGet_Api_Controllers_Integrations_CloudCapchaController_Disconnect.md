[web] DELETE /api/cloud-capcha/disconnect  (DataGet.Api.Controllers.Integrations.CloudCapchaController.Disconnect)  [L118–L126] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L123]
      └─ implementation Workpapers.Next.Services.ConnectionService.DeleteCurrentConnectionAsync [L24-L211]
        └─ uses_service ApiService (SingleInstance)
          └─ method GetAccountingFiles [L81]
            └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
  └─ impact_summary
    └─ services 1
      └─ IConnectionService


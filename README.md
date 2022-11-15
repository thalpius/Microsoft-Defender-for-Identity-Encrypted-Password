# Microsoft-Defender-for-Identity-Encrypted-Password

When installing a Microsoft Defender for Identity sensor with proxy settings, SensorConfiguration.json contains the password in an encrypted form. Using this tool you can decrypt the password or encrypt the password to add the property "EncryptedBytes" manually so you do not have to reinstall the sensor since the proxy can only be set during the installation.

I discovered that I could decrypt all passwords found for all non-gMSA accounts entered in the portal at "Directory Services Accounts" since the sensor-updater log contains all encrypted passwords for all accounts. The weird thing is that I can decrypt all passwords with a single certificate. So, once a single server is compromised, which holds the Microsoft Defender for Identity sensor, all passwords are known in plain text using this tool, including all passwords across forests or domains!

For more information please check:<br>
https://thalpius.com/2022/11/15/microsoft-defender-for-identity-encrypted-password/

Here is an example of a SensorConfiguration.json:

```json
{
  "$type": "SensorMandatoryConfiguration",
  "SecretManagerConfigurationCertificateThumbprint": "E44826515D2778493F7F1B44A4F0A435832C657B",
  "SensorProxyConfiguration": {
    "$type": "SensorProxyConfiguration",
    "Url": "http://localhost:8080",
    "UserName": "thalpius.local\\thalpius",
    "EncryptedUserPasswordData": {
      "$type": "EncryptedData",
      "EncryptedBytes": "bvzMONrdkD+/3T3cGzi5+vQp0ksjLrk2MQPv5IFKKTVMgdK6QEbpNTXyf+V1khMasBxD/zXJ5W65+c79+7GowyL/RtIAatxwMlCRxV0rUCa/DvP7PMQ7oizcO4co9ZHRv36RzOHzjgydZRN3vHVY4Pu8tSMPNh6kToL+hfA4NrCwvWy5yGVjpNSVGM6MnM/vnRzEvpXS/eTW2s/RRP9l7A3MzDUCDvxjw3KLioscvQgmWmJk2wRwCMgf/kJ4kjr05aVI/f6tHz1NxCZic4W+Iti4DYISNkj8KJEfD8jeE898WZXg2rCeSV/ysJRm02EGDLxGVK6lOLZSmrZg8Yv2pw==",
      "CertificateThumbprint": "B6DED748E000B5A62D3F4C45058E1DCF64BB55B9",
      "SecretVersion": null
    }
  },
  "WorkspaceApplicationSensorApiWebClientConfigurationServiceEndpoint": {
    "$type": "EndpointData",
    "Address": "thalpius-onmicrosoft-comsensorapi.atp.azure.com",
    "Port": 443
  }
}
```

# Screenshots

![Alt text](/Screenshots/MicrosoftDefenderForIdentityEncryptedPassword01.png?raw=true "Microsoft Defender for Identity Encrypted Password")

An example of a password encryption:
![Alt text](/Screenshots/MicrosoftDefenderForIdentityEncryptedPassword02.png?raw=true "Microsoft Defender for Identity Encrypted Password")

An example of a password decryption:
![Alt text](/Screenshots/MicrosoftDefenderForIdentityEncryptedPassword03.png?raw=true "Microsoft Defender for Identity Encrypted Password")

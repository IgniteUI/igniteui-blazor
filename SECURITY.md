# Security Policy

This document describes how to report security vulnerabilities and which versions receive security updates.

## Supported Versions

We provide security fixes for supported releases according to the following guidance:

| Version | Supported |
| --- | --- |
| Latest release | ✅ |
| Previous major release | ✅ (critical fixes only) |
| Older releases | ❌ |

If you are unsure whether your version is supported, please report the issue anyway and we will advise on next steps.

## Reporting a Vulnerability

Please **do not** open a public GitHub issue for security vulnerabilities.

Instead, report privately using one of the following methods (preferred first):

1. **GitHub Private Vulnerability Reporting (recommended)**
	- Go to the repository's **Security** tab and use **Report a vulnerability**.

2. **Email**
	- Send details to: **igniteui@infragistics.com**

3. **Support Case**
    - If you are a registered Infragistics user, you can report the vulnerability through a support case at (https://account.infragistics.com/support-cases)

If neither option is available, contact the maintainers privately. Only use the public issue tracker for **non-security** bugs.

### What to include

To help us triage quickly, include:

- A clear description of the vulnerability and its impact
- Steps to reproduce (proof-of-concept if possible)
- Affected versions and/or commit hash
- Any relevant logs or stack traces (sanitize secrets)
- Your assessment of severity (optional)
- Suggested fix or mitigation (optional)

### Sensitive information

- Do **not** include secrets, tokens, private keys, or real customer data.
- If sensitive data is required to demonstrate the issue, redact it and describe the expected format.

## Disclosure Process

After receiving a report, we aim to follow this process:

1. **Acknowledgement**: within **3 business days**
2. **Triage** (severity assessment + scope): within **7 business days**
3. **Fix development**: timeline depends on severity and complexity
4. **Release**: we will publish a patch release and/or mitigation guidance
5. **Advisory**: we may publish a GitHub Security Advisory (crediting reporters who want it)

We may request additional information during triage.

## Severity and Prioritization

We prioritize issues using impact and exploitability, informed by CVSS where appropriate:

- **Critical**: remote code execution, auth bypass, significant data exposure
- **High**: privilege escalation, major DoS, sensitive info leaks
- **Medium/Low**: limited impact, edge cases, or hard-to-exploit issues

## Coordinated Vulnerability Disclosure

We support coordinated disclosure and ask that you:

- Give us a reasonable window to fix before public disclosure
- Avoid exploiting the vulnerability beyond what is necessary to prove it exists
- Avoid actions that degrade service availability or compromise user data

## Security Updates

Security fixes may be communicated via one or more of:

- GitHub Security Advisories
- Release notes / changelog

## Acknowledgements

We appreciate responsible disclosures. If you’d like public credit, tell us how you want to be acknowledged.
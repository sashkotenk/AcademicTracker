# Laboratory Work 6: 

## 1. Project Overview

This release focuses on establishing a robust **DevOps and Quality Assurance** pipeline. The application infrastructure has been expanded to include static code analysis, distributed tracing, metric collection, and automated security scanning. The primary goal is to ensure code maintainability, observe system behavior under load, and identify security vulnerabilities.

## 2. Infrastructure Updates (Docker Compose)

To support the new observability stack, the `docker-compose.yml` configuration was extended to include the following services:

- **SonarQube:** For continuous code quality inspection.
    
- **Zipkin:** For distributed tracing and latency analysis.
    
- **Prometheus:** For scraping and storing time-series metrics.
    
- **Grafana:** For visualizing system performance via dashboards.

<img width="1461" height="705" alt="Pasted image 20260114164956" src="https://github.com/user-attachments/assets/598da2b4-fc36-43e0-8de9-d12fb6f300be" />

---

## 3. Code Quality Assessment

**Tool:** SonarQube Community Edition.

The project implemented static code analysis to evaluate technical debt, code smells, and potential bugs.

- **Integration:** Performed via `dotnet sonarscanner` CLI tool.
    
- **Metrics Analyzed:**
    
    - **Reliability:** Detection of bugs and logical errors.
        
    - **Maintainability:** Code smell detection and cyclomatic complexity analysis.
        
    - **Security:** Vulnerability scanning (SAST).
        
- **Result:** A comprehensive report generated on the local SonarQube server (`localhost:9000`).
    
<img width="1863" height="824" alt="Pasted image 20260114163500" src="https://github.com/user-attachments/assets/098ca002-da63-4d9a-8074-35fe24781684" />

---

## 4. Observability & Telemetry Implementation

The system integrates **OpenTelemetry** standards to collect and export telemetry data.

### 4.1. Distributed Tracing (Zipkin)

- **Purpose:** To track request flows and identify latency bottlenecks across the application.
    
- **Implementation:**
    
    - Configured `OpenTelemetry.Exporter.Zipkin` to send traces to `http://localhost:9411`.
        
    - **Custom Instrumentation:** Implemented a specific `ActivitySource` named `"AcademicTracker"`.
        
    - **Simulated Latency:** Created a `SimulationController` with a custom SPAN (`"HeavyWork"`) to simulate a long-running process (2000ms delay).
        
    - **Enriched Metadata:** Added custom tags (e.g., `custom.info`) to spans for better context.

<img width="1888" height="896" alt="Pasted image 20260114163515" src="https://github.com/user-attachments/assets/74df1e93-89f7-46c0-8db8-80f8048ba5af" />
<img width="1919" height="887" alt="Pasted image 20260114164519" src="https://github.com/user-attachments/assets/abdc6d0b-54f7-4db0-87f5-5932b84099b3" />


### 4.2. Metrics & Visualization (Prometheus + Grafana)

- **Purpose:** To monitor runtime performance and resource usage.
    
- **Implementation:**
    
    - Exposed metrics via `/metrics` endpoint using `OpenTelemetry.Exporter.Prometheus.AspNetCore`.
        
    - **Collected Metrics:**
        
        - CPU Usage.
            
        - Memory / Heap Size.
            
        - Garbage Collection (GC) generations count.
            
        - HTTP Request Duration.
            
- **Visualization:** Configured Grafana (`localhost:3000`) to query Prometheus and display real-time dashboards.
    

---

## 5. Load Testing & Performance Analysis

**Tool:** Apache JMeter.

### 5.1. Methodology

1. **Data Seeding:** A specialized endpoint (`/Journal/Seed`) was implemented to programmatically insert over **1000 student records** into the database to ensure realistic test conditions.
    
2. **Test Strategy:** Conducted stress testing with varying concurrency levels: **1, 5, 20, 50, 100, and 300 simultaneous users**.
    
3. **Authentication:** Auth0/Security was temporarily disabled (`[AllowAnonymous]`) to facilitate automated load generation.

<img width="1521" height="861" alt="Pasted image 20260114163300" src="https://github.com/user-attachments/assets/58379c37-7e74-46db-b843-97704df0fceb" />


### 5.2. Analysis Results

- **Response Time Function:** Analyzed the relationship between the number of users and response time (Latency vs. Throughput).
    
- **Bottleneck Identification:** Used Zipkin to pinpoint the "slowest part" of the application (demonstrated via the `/Simulation/SlowOperation` endpoint).
    
- **Error Rate:** Monitored via JMeter Summary Report to ensure system stability under high load.
    

---

## 6. Security Testing

**Tool:** OWASP ZAP (Zed Attack Proxy).

An automated security scan was performed against the running application (`https://localhost:7001`) to detect common web vulnerabilities.

- **Scan Type:** Automated Scan.
    
- **Scope:** SQL Injection, XSS (Cross-Site Scripting), Security Headers, and Information Disclosure.
    
- **Deliverable:** A generated HTML report detailing identified alerts and risk levels.
    

---

## 7. How to Run (Validation Steps)

1. **Start Infrastructure:**
    
    Bash
    
    ```
    docker-compose up -d
    ```
    
2. **Run Application:**
    
    Bash
    
    ```
    cd Tracker.Web
    dotnet run
    ```
    
3. **Access Services:**
    
    - **App:** `https://localhost:7001`
        
    - **SonarQube:** `http://localhost:9000`
        
    - **Grafana:** `http://localhost:3000`
        
    - **Zipkin:** `http://localhost:9411`
        
4. **Execute Tests:**
    
    - Open `JMeter`, load the test plan, and click "Start".
        
    - Run OWASP ZAP "Automated Scan" against the localhost URL.

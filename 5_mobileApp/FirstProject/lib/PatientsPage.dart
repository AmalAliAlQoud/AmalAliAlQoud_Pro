import 'package:flutter/material.dart';
import 'data/PatientsServices.dart';

class patientspage extends StatefulWidget {
  const patientspage({super.key});

  @override
  State<patientspage> createState() => _patientspageState();
}

class _patientspageState extends State<patientspage> {
  // تعريف كلاس الخدمة للمرضى
  final PatientsServices patientsServices = PatientsServices();

  late Future<List<dynamic>> patients;

  @override
  void initState() {
    super.initState();
    patients = patientsServices.getPatients();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Patients'),
        backgroundColor: Colors.deepPurple,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: patients,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text('Error: ${snapshot.error}'),
            );
          }

          final patientsList = snapshot.data ?? [];

          if (patientsList.isEmpty) {
            return const Center(
              child: Text('No Patients Found'),
            );
          }

          return GridView.builder(
            padding: const EdgeInsets.all(12),
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 0.75,
            ),
            itemCount: patientsList.length,
            itemBuilder: (context, index) {
              final patient = patientsList[index];

              // استخراج بيانات المريض بدقة من الـ JSON
              final String name = patient['name']?.toString() ?? 'Unknown Patient';
              final String phone = patient['phone']?.toString() ?? 'No Phone';
              final String dob = patient['date_of_Birth']?.toString() ?? 'No Date';

              return InkWell(
                onTap: () {
                  // الانتقال لصفحة تفاصيل المريض مستقبلاً
                },
                child: Card(
                  elevation: 4,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Column(
                    children: [
                      const Expanded(
                        child: ClipRRect(
                          borderRadius: BorderRadius.vertical(
                            top: Radius.circular(15),
                          ),
                          child: Center(
                            child: Icon(
                              Icons.person,
                              size: 60,
                              color: Colors.deepPurple,
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 10),
                      // اسم المريض
                      Text(
                        name,
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 5),
                      // رقم الهاتف
                      Text(
                        phone,
                        style: const TextStyle(
                          fontSize: 13,
                          color: Colors.grey,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                      const SizedBox(height: 3),
                      // تاريخ الميلاد
                      Text(
                        'DOB: $dob',
                        style: const TextStyle(
                          fontSize: 12,
                          color: Colors.blueGrey,
                        ),
                      ),
                      const SizedBox(height: 10),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}
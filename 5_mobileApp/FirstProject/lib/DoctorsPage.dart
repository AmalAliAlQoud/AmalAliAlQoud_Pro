import 'package:flutter/material.dart';
import 'DoctorsDetails.dart';
import 'data/DoctorService.dart';
import 'data/ConectionString.dart';

class doctorspage extends StatefulWidget {
  const doctorspage({super.key});

  @override
  State<doctorspage> createState() => _doctorspageState();
}

class _doctorspageState extends State<doctorspage> {
  // تعريف كلاس الخدمة مرة واحدة فقط
  final DoctorService doctorService = DoctorService();

  late Future<List<dynamic>> doctors;

  @override
  void initState() {
    super.initState();
    doctors = doctorService.getDoctors();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Doctors'),
        backgroundColor: Colors.deepPurple,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: doctors,
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

          // تغيير التسمية لتعبر عن قائمة الأطباء
          final doctorsList = snapshot.data ?? [];

          if (doctorsList.isEmpty) {
            return const Center(
              child: Text('No Doctors Found'),
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
            itemCount: doctorsList.length,
            itemBuilder: (context, index) {
              final doctor = doctorsList[index];

              // رابط الصورة الخاص بالطبيب من قاعدة البيانات
              final String imagePath =
                  doctor['imageUrl']?.toString() ?? '';

              final String imageUrl = imagePath.isEmpty
                  ? ''
                  : Uri.parse(ConectionString.dashboardUrl)
                  .resolve(imagePath)
                  .toString();

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => DoctorsDetails(doctor),
                    ),
                  );
                },
                child: Card(
                  elevation: 4,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Column(
                    children: [
                      Expanded(
                        child: ClipRRect(
                          borderRadius: const BorderRadius.vertical(
                            top: Radius.circular(15),
                          ),
                          child: imageUrl.isNotEmpty
                              ? Image.network(
                            imageUrl,
                            width: double.infinity,
                            fit: BoxFit.cover,
                            errorBuilder: (context, error, stackTrace) {
                              return const Center(
                                child: Icon(
                                  Icons.person,
                                  size: 60,
                                ),
                              );
                            },
                          )
                              : const Center(
                            child: Icon(
                              Icons.person,
                              size: 60,
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 10),
                      // اسم الطبيب (تأكدي أن اسم الحقل في الـ API كـ 'name' أو 'fullName')
                      Text(
                        doctor['name']?.toString() ?? 'Unknown Doctor',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 5),
                      // تخصص الطبيب أو القسم
                      Text(
                        doctor['specialtyName'] ?? 'General',
                        style: const TextStyle(
                          fontSize: 14,
                          color: Colors.grey,
                          fontWeight: FontWeight.w600,
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